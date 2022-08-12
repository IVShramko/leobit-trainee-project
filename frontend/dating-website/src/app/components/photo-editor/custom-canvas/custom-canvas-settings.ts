import { PaintingMode } from './painting-mode';

export interface ICustomCanvasSettings {
    mode: PaintingMode,
    isActive: boolean,
    size: number,
    style: string,
    color: string
}