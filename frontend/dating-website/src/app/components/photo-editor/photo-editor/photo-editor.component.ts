import { ImageUtility } from './../../../utilities/image-utility';
import { tap } from 'rxjs';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { PhotoMainDTO } from 'src/app/models/photoMainDTO';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CustomCanvas } from '../custom-canvas/custom-сanvas';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private photoService: PhotoService,
    private router: Router,
    private imageUtility: ImageUtility) {
  }

  @ViewChild('canvas') canvasElem: ElementRef;
  photo: PhotoMainDTO;
  customCanvas: CustomCanvas;

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.photoService.GetPhotoById(params.id)
          .pipe(
            tap((photo: PhotoMainDTO) => this.photo = photo),
            tap(() => this.InitCanvas()),
          ).subscribe();
      });
  }

  private InitCanvas() {
    this.customCanvas = new CustomCanvas(this.canvasElem.nativeElement);
    const photo = new Image();
    
    photo.src = this.imageUtility
      .ConvertToStringUrl(this.photo.data, this.photo.name);

    photo.onload = () =>
      this.customCanvas?.SetCanvasImage(photo as CanvasImageSource);
  }

  ApplyChanges() {
    const data = <string>this.customCanvas?.GetDataUrl();

    const updatedPhoto: PhotoMainDTO = {
      id: this.photo.id,
      albumId: this.photo.albumId,
      name: this.photo.name,
      data: data
    }

    this.photoService.UpdatePhotoDataUrl(updatedPhoto)
      .pipe(
        tap(() => this.router
          .navigate(['account', 'albums', this.photo.albumId]))
      ).subscribe();
  }

}
