import { IChatFullDTO } from './../../models/chat/chat-full-dto';
import { MessageDeliveryStatus } from './../../enums/message-delivery-status';
import { Observable } from 'rxjs';
import { IChatMessageDTO } from './../../models/chat/chat-message-dto';
import { SignalRService } from './../signalr-service/signalr.service';
import { Injectable } from '@angular/core';
import { ChatMehods } from 'src/app/enums/chat-methods';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {

  constructor(private signalRService: SignalRService) { }

  SendMessage(message: IChatMessageDTO) {
    return new Observable<boolean>((subscriber) => {
      this.signalRService.connection.invoke(ChatMehods.SendToUser, message);

      this.signalRService.connection.on(
        ChatMehods.GetMessageDeliveryStatus, (status: MessageDeliveryStatus) => {
          this.signalRService.connection.off(ChatMehods.GetMessageDeliveryStatus);

          const result = status === MessageDeliveryStatus.Sent;
          subscriber.next(result);
        });
    });
  }

  ReceiveMessage() {
    return new Observable<IChatMessageDTO>((subscriber) => {
      this.signalRService.connection.on(
        ChatMehods.ReceiveMessage, (message: IChatMessageDTO) => {
          subscriber.next(message);
      });
    });


  }
}
