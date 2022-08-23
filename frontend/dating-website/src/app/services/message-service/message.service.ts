import { IChatMessageDTO } from './../../models/chat/chat-message-dto';
import { MESSAGE_PATH } from './../../paths';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IChatMessageCreateDTO } from 'src/app/models/chat/chat-message-create-dto';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private server: HttpClient) { }

  private readonly path = MESSAGE_PATH;

  GetAllChatMessages(chatId: string) {
    return this.server.get<IChatMessageDTO[]>(this.path + chatId);
  }

  CreateMessage(newMessage: IChatMessageCreateDTO) {
    return this.server.post<boolean>(this.path, newMessage);
  }


}
