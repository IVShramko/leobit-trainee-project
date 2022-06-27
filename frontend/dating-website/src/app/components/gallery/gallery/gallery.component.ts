import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Album } from 'src/app/models/Album';
import { GalleryService } from 'src/app/services/gallery-service/gallery.service';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  constructor(private galleryService: GalleryService,
    private router: Router, private route: ActivatedRoute) { }

  albums: Album[];

  ngOnInit(): void {
    this.galleryService.GetAllAlbums().subscribe(
      (albums) => this.albums = albums
    );
  }

  CreateAlbum()
  {
    const album = {
      id: null,
      userProfileId: null,
      name: 'newAlbum'
    }
    this.albums.push(album);
  }

  OpenAlbum(album: Album)
  {
    this.router.navigate([album.name], 
      {relativeTo: this.route, queryParams: {id: album.id}});
  }

}
