import { AlbumFull } from './../../models/AlbumFull';
import { AlbumCreate } from './../../models/AlbumCreate';
import { ALBUM_PATH } from './../../Paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AlbumMain } from 'src/app/models/AlbumMain';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  constructor(private server: HttpClient) { }

  path: string = ALBUM_PATH;

  CheckNameValidity(name: string): Observable<boolean>
  {
    return this.server.post<boolean>(this.path + 'check', null, {params: {name: name}});
  }

  GetAllAlbums(): Observable<AlbumMain[]>
  {
    return this.server.get<AlbumMain[]>(this.path);
  }

  GetAlbumById(id: string): Observable<AlbumFull>
  {
    return this.server.get<AlbumFull>(this.path + id);
  }

  CreateAlbum(newAlbum: AlbumCreate): Observable<boolean>
  {
    return this.server.post<boolean>(this.path, newAlbum);
  }

  UpdateAlbum(album: AlbumFull): Observable<boolean>
  {
    return this.server.put<boolean>(this.path, album);
  }

  DeleteAlbum(id: string)
  {
    return this.server.delete(this.path + id);
  }
}
