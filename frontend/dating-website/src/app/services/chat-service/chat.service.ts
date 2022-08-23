import { IChatFullDTO } from './../../models/chat/chat-full-dto';
import { Observable } from 'rxjs';
import { IChatShortDTO } from './../../models/chat/chat-short-dto';
import { CHAT_PATH } from './../../paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private server: HttpClient) { }

  private readonly path: string = CHAT_PATH;

  GetAllChats(): Observable<IChatShortDTO[]> {
    return this.server.get<IChatShortDTO[]>(this.path);
  }

  GetChatById(id: string): Observable<IChatFullDTO> {
    return this.server.get<IChatFullDTO>([this.path, id].join(''));
  }

  CreateChat() {

  }
}
