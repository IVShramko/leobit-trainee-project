import { ActivatedRoute, Params } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Photo } from 'src/app/models/Photo';
import { GalleryService } from 'src/app/services/gallery-service/gallery.service';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  name: string;
  id: string;
  photos: Photo[];

  constructor(private route: ActivatedRoute,
    private galleryService: GalleryService) { }

  ngOnInit(): void {
    this.route.params.subscribe(
      (params: Params) => {
        this.name = params.name
      }
    );
    this.route.queryParams.subscribe(
      (queryParams) => {
        this.id = queryParams.id;
        
        this.galleryService.GetAllPhotos(this.id).subscribe(
          (photos) => this.photos = photos
        )
      }
    );
  }

}
