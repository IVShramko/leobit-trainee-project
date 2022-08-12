export interface IDrawer{
    Draw(event: MouseEvent): void;
    EndDraw(): void;
    StartDraw(event: MouseEvent): void;
}