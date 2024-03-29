import { ImageUtility } from './../../../utilities/image-utility';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormControl, ValidatorFn, Validators } from '@angular/forms';
import { PhotoCreateDTO } from '../../../models/photoCreateDTO';
import { SafeResourceUrl } from '@angular/platform-browser';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { AlbumFullDTO } from '../../../models/albumFullDTO';
import { ActivatedRoute, Params } from '@angular/router';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AlbumService } from 'src/app/services/album-service/album.service';
import { PhotoMainDTO } from 'src/app/models/photoMainDTO';
import { CustomValidatorsService } from 'src/app/services/custom-validators/custom-validators.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  @ViewChild('carouselActive') currentPhoto: ElementRef;

  album: AlbumFullDTO;
  photos: PhotoMainDTO[];

  photoInput: FormControl;

  carouselViewActivated: boolean;
  focusedPhoto: PhotoMainDTO;

  newImage: PhotoCreateDTO = {
    albumId: '',
    name: '',
    data: ''
  };

  constructor(private route: ActivatedRoute,
    private albumService: AlbumService,
    private photoService: PhotoService,
    private formBuilder: FormBuilder,
    private customValidatorsService: CustomValidatorsService,
    private imageUtility: ImageUtility) {
  }

  ngOnInit(): void {

    this.photoInput = this.formBuilder.control(null, {
      validators: [
        <ValidatorFn>this.customValidatorsService.FileTypeValidator,
        Validators.required
      ]
    })
    this.route.params.subscribe(
      (params: Params) => {
        this.albumService.GetAlbumById(params.id).subscribe(
          (album) => {
            this.album = album
            this.LoadPhotos(this.album.id)
          });
      });
  }

  get photoControl() {
    return this.photoInput;
  }

  private LoadPhotos(albumId: string) {
    this.photoService.GetAllPhotos(albumId).subscribe(
      (photos) => this.photos = photos
    )
  }

  async OnFileUpload(event: Event) {
    const input = event.currentTarget as HTMLInputElement;
    const file = input.files?.item(0);

    if (file) {
      this.newImage.data = await this.imageUtility
        .ConvertToBase64(file) as string;
      this.newImage.name = input.value?.split('\\').pop() as string;
      this.newImage.albumId = this.album.id;
    }
  }

  ConvertToImage(base64: string, name: string): SafeResourceUrl {
    return this.imageUtility.ConvertToSafeResourceUrl(base64, name);
  }

  AddPhoto() {
    this.photoService.CreatePhoto(this.newImage).subscribe(
      (res) => this.LoadPhotos(this.album.id),
      (error: HttpErrorResponse) => alert('this photo is already uploaded'),
      () => this.photoControl.reset()
    );
  }

  OnPhotoDelete(id: string) {
    this.photoService.DeletePhoto(id).subscribe(
      (res) => this.LoadPhotos(this.album.id)
    )
  }

  OnActivateCarouselView(photo: PhotoMainDTO) {
    this.ToggleCarouselView();
    this.focusedPhoto = photo;
  }

  ToggleCarouselView() {
    this.carouselViewActivated = !this.carouselViewActivated;
  }
}
