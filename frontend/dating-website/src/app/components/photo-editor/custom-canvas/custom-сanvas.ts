import { DrawerFactory } from './drawer-factory';
import { ICustomCanvasSettings } from "./custom-canvas-settings";
import { PaintingMode } from "./painting-mode";
import { IDrawer } from './idrawer';

export class CustomCanvas {

    private canvas: HTMLCanvasElement;
    private context2D: CanvasRenderingContext2D;
    private _settings: ICustomCanvasSettings;
    private drawerFactory: DrawerFactory;
    private drawer: IDrawer;

    constructor(defaultCanvas: HTMLCanvasElement) {
        this.canvas = defaultCanvas;
        this.context2D = <CanvasRenderingContext2D>defaultCanvas.getContext('2d');
        this.drawerFactory = new DrawerFactory();
        this.SetDefaultSettings();
        this.SetDrawerOnMode();
    }

    get settings() {
        return this._settings;
    }

    private ApplySettings() {
        this.context2D.strokeStyle = this._settings.color;
        this.context2D.lineCap = <CanvasLineCap>this._settings.style;
        this.context2D.lineWidth = this._settings.size;
    }

    private SetDefaultSettings() {
        this._settings = {
            mode: PaintingMode.none,
            isActive: false,
            size: 10,
            style: 'round',
            color: '#FF0000'
        }
    }

    private SetDrawerOnMode() {
        const drawer = this.drawerFactory
            .CreateDrawerOnMode(this._settings.mode, this.context2D);
        this.drawer = drawer as IDrawer;
    }

    DoAction(event: MouseEvent) {
        this.ApplySettings();
        if (this._settings.isActive) {
            this.drawer.Draw(event);
        }
    }

    EndAction(event: MouseEvent) {
        if (this.drawer !== undefined) {
            this._settings.isActive = false;
            this.drawer.EndDraw();
        }
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

    SetCanvasSize(sizeX: number, sizeY: number) {
        this.canvas.width = sizeX;
        this.canvas.height = sizeY;
    }

    SetCanvasImage(image: CanvasImageSource) {
        this.SetCanvasSize(image.width as number, image.height as number);
        this.context2D.drawImage(image, 0, 0);
    }

    SetColor(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._settings.color = elem.value;
    }

    SetSize(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._settings.size = +elem.value;
    }

    SetStyle(event: Event) {
        const elem = <HTMLInputElement>event.target;
        this._settings.style = <CanvasLineCap>elem.value;
    }

    StartAction(event: MouseEvent) {
        if (this.drawer !== undefined) {
            this._settings.isActive = true;
            this.drawer?.StartDraw(event);
        }
    }

    ToggleMode(mode: PaintingMode) {
        if (mode === this._settings.mode) {
            this._settings.mode = PaintingMode.none;
        } else {
            this._settings.mode = mode;
        }
        this.SetDrawerOnMode();
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

        this.context2D.translate(this.canvas.width / 2, this.canvas.height / 2)

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

        this.context2D.translate(this.canvas.width / 2, this.canvas.height / 2)

        this.canvas.width *= 0.5;
        this.canvas.height *= 0.5;

        this.context2D.scale(0.5, 0.5)
    }
}