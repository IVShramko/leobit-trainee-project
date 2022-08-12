import { IDrawer } from './idrawer';
export class FigureDrawer implements IDrawer{

    private context2D: CanvasRenderingContext2D;
    private cordsX: number[] = [];
    private cordsY: number[] = [];

    constructor(context: CanvasRenderingContext2D) {
        this.context2D = context;
    }

    StartDraw(event: MouseEvent) {
        this.cordsX.push(event.offsetX);
        this.cordsY.push(event.offsetY);
    }

    Draw(event: MouseEvent) {
        this.cordsX.push(event.offsetX);
        this.cordsY.push(event.offsetY);

        this.context2D?.beginPath();
        this.context2D?.moveTo(this.cordsX[0], this.cordsY[0]);
        this.context2D
            .lineTo(this.cordsX[0], this.cordsY[this.cordsY.length - 1]);
        this.context2D?.stroke();

        this.context2D?.beginPath();
        this.context2D?.moveTo(this.cordsX[0], this.cordsY[0]);
        this.context2D
            .lineTo(this.cordsX[this.cordsX.length - 1], this.cordsY[0]);
        this.context2D?.stroke();
    }

    EndDraw() {
        this.context2D?.beginPath();
        this.context2D.moveTo(
            this.cordsX[this.cordsX.length - 1],
            this.cordsY[this.cordsY.length - 1]);

        this.context2D
            .lineTo(this.cordsX[0], this.cordsY[this.cordsY.length - 1]);
        this.context2D?.stroke();

        this.context2D?.beginPath();
        this.context2D.moveTo(
            this.cordsX[this.cordsX.length - 1],
            this.cordsY[this.cordsY.length - 1]);

        this.context2D
            .lineTo(this.cordsX[this.cordsY.length - 1], this.cordsY[0]);

        this.context2D?.stroke();

        this.cordsX = [];
        this.cordsY = [];
    }

}