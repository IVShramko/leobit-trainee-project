import { MessageStatuses } from '../../enums/message-delivery-statuses';
import { Observable, Subject } from 'rxjs';
import { IChatMessageDTO } from './../../models/chat/chat-message-dto';
import { SignalRService } from './../signalr-service/signalr.service';
import { Injectable } from '@angular/core';
import { ChatMehods } from 'src/app/enums/chat-methods';
import { Hubs } from 'src/app/enums/hubs';
import { HubConnection } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {

  private chatConnection: Observable<HubConnection> = new Observable<HubConnection>((subscriber) => {
    const isOpened = this.signalRService.IsHubConnectionOpened(Hubs.Chat);

    if (isOpened) {
      subscriber.next(this.signalRService.GetHubConnection(Hubs.Chat))
    } else {
      this.isConnectionReady.subscribe(() => {
        subscriber.next(this.signalRService.GetHubConnection(Hubs.Chat))
      });
    }
  });

  private isConnectionReady = new Subject<boolean>();

  constructor(private signalRService: SignalRService) {
    this.signalRService.StartHubConnection(Hubs.Chat)
      .then(() => {
        this.isConnectionReady.next(true)
      })
      .catch(() => {
        this.isConnectionReady.next(false)
      });
  }

  SendMessage(message: IChatMessageDTO) {
    return new Observable<boolean>((subscriber) => {
      this.chatConnection
        .subscribe((connection) => {
          connection.invoke(ChatMehods.SendToUser, message)
            .then((result: MessageStatuses) => {
              switch (result) {
                case MessageStatuses.Sent:
                  subscriber.next(true);
                  break;
                case MessageStatuses.Error:
                  subscriber.next(false);
                  break;
              }
            })
        })
    });
  }

  ReceiveMessage() {
    return new Observable<IChatMessageDTO>((subscriber) => {
      this.chatConnection.subscribe((connection) => {
        connection?.on(
          ChatMehods.ReceiveMessage, (message: IChatMessageDTO) => {
            subscriber.next(message);
          });
      })
    });
  }
}
