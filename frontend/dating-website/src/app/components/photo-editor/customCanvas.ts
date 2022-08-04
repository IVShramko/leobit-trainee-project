
export class CustomCanvas {

    private canvas: HTMLCanvasElement;
    private context2D: CanvasRenderingContext2D;

    private isPainting: boolean;

    cordsX: number[] = [];
    cordsY: number[] = [];

    constructor(defaultCanvas: HTMLCanvasElement) {
        this.canvas = defaultCanvas;
        this.context2D =
            <CanvasRenderingContext2D>defaultCanvas.getContext('2d');
    }

    EndPainting() {
        this.isPainting = false;
        this.cordsX = [];
        this.cordsY = [];
    }

    GetDataUrl(): string {
        return this.canvas.toDataURL();
    }

    Rotate(angle: number) {
        const img = new Image();
        img.src = this.canvas.toDataURL();
        
        img.onload = () => this.context2D.drawImage(img, -img.width / 2, -img.height / 2);

        this.context2D.save();

        [this.canvas.width, this.canvas.height] =
            [this.canvas.height, this.canvas.width];

        this.context2D.restore();
        this.context2D.translate(this.canvas.width / 2, this.canvas.height / 2);
        this.context2D.rotate(angle * Math.PI / 180);
    }

    Paint(event: MouseEvent) {
        if (this.isPainting) {

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
        this.context2D.strokeStyle = elem.value;
    }

    SetBrushSize(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this.context2D.lineWidth = +elem.value;
    }

    SetBrushStyle(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this.context2D.lineCap = <CanvasLineCap>elem.value;
    }

    StartPainting(event: MouseEvent) {
        this.isPainting = true;
        this.cordsX.push(event.offsetX)
        this.cordsY.push(event.offsetY)
    }
}