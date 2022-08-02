import { AlbumFullDTO } from '../../models/albumFullDTO';
import { AlbumCreateDTO } from '../../models/albumCreateDTO';
import { ALBUM_PATH } from '../../paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AlbumMainDTO } from 'src/app/models/albumMainDTO';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  constructor(private server: HttpClient) { }

  path: string = ALBUM_PATH;

  CheckNameValidity(name: string): Observable<boolean> {
    return this.server.post<boolean>(this.path + 'check', null, { params: { name: name } });
  }

  GetAllAlbums(): Observable<AlbumMainDTO[]> {
    return this.server.get<AlbumMainDTO[]>(this.path);
  }

  GetAlbumById(id: string): Observable<AlbumFullDTO> {
    return this.server.get<AlbumFullDTO>(this.path + id);
  }

  CreateAlbum(newAlbum: AlbumCreateDTO): Observable<boolean> {
    return this.server.post<boolean>(this.path, newAlbum);
  }

  UpdateAlbum(album: AlbumFullDTO): Observable<boolean> {
    return this.server.put<boolean>(this.path, album);
  }

  DeleteAlbum(id: string) {
    return this.server.delete(this.path + id);
  }
}
