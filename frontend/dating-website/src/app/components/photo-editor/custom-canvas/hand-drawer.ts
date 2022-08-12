import { IDrawer } from "./idrawer";

export class HandDrawer implements IDrawer{

    private cordsX: number[] = [];
    private cordsY: number[] = [];

    private context2D: CanvasRenderingContext2D;

    constructor(context: CanvasRenderingContext2D) {
        this.context2D = context;
    }

    EndDraw() {
        this.cordsX = [];
        this.cordsY = [];
    }

    Draw(event: MouseEvent) {
        this.cordsX.push(event.offsetX);
        this.cordsY.push(event.offsetY);

        this.context2D?.beginPath();
        this.context2D.moveTo(this.cordsX[0], this.cordsY[0]);
        this.context2D?.lineTo(event.offsetX, event.offsetY);
        this.context2D?.stroke();

        this.cordsX.shift();
        this.cordsY.shift();
    }

    StartDraw(event: MouseEvent) {
        this.cordsX.push(event.offsetX)
        this.cordsY.push(event.offsetY)
    }
}