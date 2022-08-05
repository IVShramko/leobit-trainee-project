import { ICustomCanvasOptions } from "./photo-editor/ICustomCanvasOptions";
import { PaintingMode } from "./photo-editor/paintingMode";

export class CustomCanvas {

    private canvas: HTMLCanvasElement;
    private context2D: CanvasRenderingContext2D;
    private _options: ICustomCanvasOptions;
    cordsX: number[] = [];
    cordsY: number[] = [];

    constructor(defaultCanvas: HTMLCanvasElement) {
        this.canvas = defaultCanvas;
        this.context2D =
            <CanvasRenderingContext2D>defaultCanvas.getContext('2d');

        this.SetDefaultOptions();
    }

    get options() {
        return this._options;
    }

    private ApplyOptions() {
        this.context2D.strokeStyle = this._options.color;
        this.context2D.lineCap = <CanvasLineCap>this._options.style;
        this.context2D.lineWidth = this._options.size;
    }

    private SetDefaultOptions() {
        this._options = {
            mode: PaintingMode.none,
            isActive: false,
            size: 10,
            style: 'round',
            color: '#FF0000'
        }
    }

    EndPainting() {
        this._options.isActive = false;
        this.cordsX = [];
        this.cordsY = [];
    }

    GetDataUrl(): string {
        return this.canvas.toDataURL();
    }

    Rotate(angle: number) {
        const img = new Image();
        img.src = this.canvas.toDataURL();

        img.onload = () => {
            this.context2D.drawImage(img, -img.width / 2, -img.height / 2);
            this.context2D.setTransform(1, 0, 0, 1, 0, 0);
        }

        this.context2D.save();

        [this.canvas.width, this.canvas.height] =
            [this.canvas.height, this.canvas.width];

        this.context2D.restore();
        this.context2D.translate(this.canvas.width / 2, this.canvas.height / 2);
        this.context2D.rotate(angle * Math.PI / 180);

    }

    Paint(event: MouseEvent) {
        if (this._options.isActive &&
            this._options.mode === PaintingMode.brush) {

            this.ApplyOptions();

            this.cordsX.push(event.offsetX);
            this.cordsY.push(event.offsetY);

            this.context2D?.beginPath();
            this.context2D.moveTo(this.cordsX[0], this.cordsY[0]);
            this.context2D?.lineTo(event.offsetX, event.offsetY);
            this.context2D?.stroke();

            this.cordsX.shift();
            this.cordsY.shift();
        }
    }

    SetCanvasSize(sizeX: number, sizeY: number) {
        this.canvas.width = sizeX;
        this.canvas.height = sizeY;
    }

    SetCanvasImage(image: CanvasImageSource) {
        this.canvas.height = image.height as number;
        this.canvas.width = image.width as number;

        this.context2D.drawImage(image, 0, 0);
    }

    SetColor(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._options.color = elem.value;
    }

    SetBrushSize(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._options.size = +elem.value;
    }

    SetBrushStyle(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._options.style = <CanvasLineCap>elem.value;
    }

    ToggleMode(mode: PaintingMode) {
        if (mode === this._options.mode) {
            this._options.mode = PaintingMode.none;
            return;
        }

        this._options.mode = mode;
    }

    StartPainting(event: MouseEvent) {
        this._options.isActive = true;
        this.cordsX.push(event.offsetX)
        this.cordsY.push(event.offsetY)
    }

    ZoomIn() {
        //this.context2D.webkitImageSmoothingEnabled = false;
        //this.context2D.mozImageSmoothingEnabled = false;
        this.context2D.imageSmoothingEnabled = false;

        const img = new Image();
        img.src = this.canvas.toDataURL();

        img.onload = () => {
            this.context2D.drawImage(img, 0, 0);
            this.context2D.setTransform(1, 0, 0, 1, 0, 0);
        }

        this.context2D.translate(this.canvas.width/2, this.canvas.height/2)

        this.canvas.width *= 2;
        this.canvas.height *= 2;

        this.context2D.scale(2, 2)
    }

    ZoomOut() {
        const img = new Image();
        img.src = this.canvas.toDataURL();

        img.onload = () => {
            this.context2D.drawImage(img, 0, 0);
            this.context2D.setTransform(1, 0, 0, 1, 0, 0);
        }

        this.context2D.translate(this.canvas.width/2, this.canvas.height/2)

        this.canvas.width *= 0.5;
        this.canvas.height *= 0.5;

        this.context2D.scale(0.5, 0.5)
    }
}