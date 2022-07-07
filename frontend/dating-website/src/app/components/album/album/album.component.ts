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

  constructor(private route: ActivatedRoute,
    private albumService: AlbumService,
    private photoService: PhotoService) { }

  ngOnInit(): void {

    this.route.params.subscribe(
      (params: Params) => {
        
        this.albumService.GetAlbumById(params.id).subscribe(
          (album) => {
            this.album = album

            this.photoService.GetAllPhotos(album.id).subscribe(
              (photos) => this.photos = photos
            )});
      });
  }

}
