import { PhotoCreate } from './../../../models/PhotoCreate';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { PhotoService } from './../../../services/photo-service/photo.service';
import { AlbumFull } from './../../../models/AlbumFull';
import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AlbumService } from 'src/app/services/album-service/album.service';
import { PhotoMain } from 'src/app/models/PhotoMain';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  album: AlbumFull;
  photos: PhotoMain[];
  
  newImage: PhotoCreate = {
    albumId: '',
    name: '',
    base64: ''
  };

  constructor(private route: ActivatedRoute,
    private albumService: AlbumService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        
        this.albumService.GetAlbumById(params.id).subscribe(
          (album) => {
            this.album = album
            this.LoadPhotos(this.album.id)
            });
      });
  }

  private LoadPhotos(albumId: string)
  {
    this.photoService.GetAllPhotos(albumId).subscribe(
      (photos) => this.photos = photos
    )
  }

  async OnFileUpload(event: Event)
  {
    const input = event.currentTarget as HTMLInputElement;
    const file = input.files?.item(0);

    if(file)
    {
      this.newImage.base64 = await this.ConvertToBase64(file) as string;
      this.newImage.name = input.value?.split('\\').pop() as string;
      this.newImage.albumId = this.album.id;
    }
  }

  AddPhoto()
  {
    this.photoService.CreatePhoto(this.newImage).subscribe(
      (res) => this.LoadPhotos(this.album.id)
    );
  }

  OnPhotoDelete(id: string)
  {
    this.photoService.DeletePhoto(id).subscribe(
      (res) => this.LoadPhotos(this.album.id)
    )
  }

  private ConvertToBase64(file: File)
  {
    return new Promise((resolve, reject) => 
    {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  }

  ConvertToImage(base64: string, name: string): SafeResourceUrl
  {
    const extension = name.split('.').shift();
    
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/${extension};base64,` + base64 as string);
  }
}
