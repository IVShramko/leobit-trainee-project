import { Observable, of } from 'rxjs';
import { GALLERY_PATH } from './../../Paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Album } from 'src/app/models/Album';
import { Photo } from 'src/app/models/Photo';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {

  private readonly path: string = GALLERY_PATH;

  constructor(private server: HttpClient) { }

  GetAllAlbums(): Observable<Album[]>
  {
    const albums:Album[] = [{
      id: '1',
      name: 'Album1'
    },{
      id: '2',
      name: 'Album2'
    }];

    return new Observable<Album[]>((observer) => observer.next(albums))

  }

  // GetAllAlbums(userId: string): Observable<Album[]>
  // {
  //   return this.server.get<Album[]>(this.path + '/albums');
  // }

  GetAllPhotos(albumId: string): Observable<Photo[]>
  {
    return this.server.get<Photo[]>(this.path + '/photos');
  }

}
