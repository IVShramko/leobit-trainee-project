import { HandDrawer } from './hand-drawer';
import { IDrawer } from './idrawer';
import { PaintingMode } from './painting-mode';
import { FigureDrawer } from './figure-drawer';


export class DrawerFactory {
    CreateDrawerOnMode(
        mode: PaintingMode,
        context: CanvasRenderingContext2D): IDrawer | undefined {

        let drawer: IDrawer | undefined;
        switch (mode) {
            case PaintingMode.brush:
                drawer = new HandDrawer(context);
                break;
            case PaintingMode.figure:
                drawer = new FigureDrawer(context);
                break;
            default:
                drawer = undefined
                break;
        }

        return drawer;
    }
}