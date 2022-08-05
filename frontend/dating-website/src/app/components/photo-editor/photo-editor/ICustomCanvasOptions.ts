import { PaintingMode } from './paintingMode';
export interface ICustomCanvasOptions {
    mode: PaintingMode,
    isActive: boolean,
    size: number,
    style: string,
    color: string
}