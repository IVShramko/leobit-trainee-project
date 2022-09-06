import { HubConnection } from '@microsoft/signalr';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ChatMehods } from 'src/app/enums/chat-methods';
import { Hubs } from 'src/app/enums/hubs';
import { SignalRService } from '../signalr-service/signalr.service';


@Injectable({
  providedIn: 'root'
})
export class StatusService {

  private statusConnection: Observable<HubConnection> = new Observable<HubConnection>((subscriber) => {
    const isOpened = this.signalRService.IsHubConnectionOpened(Hubs.Status);

    if (isOpened) {
      subscriber.next(this.signalRService.GetHubConnection(Hubs.Status))
    } else {
      this.isConnectionReady.subscribe(() => {
        subscriber.next(this.signalRService.GetHubConnection(Hubs.Status))
      });
    }
  });

  private isConnectionReady = new Subject<boolean>();

  constructor(private signalRService: SignalRService) {
    this.signalRService.StartHubConnection(Hubs.Status)
      .then(() => {
        this.isConnectionReady.next(true)
      })
      .catch(() => {
        this.isConnectionReady.next(false)
      });
  }

  GetUserStatus() {
    return new Observable<{ userId: string, status: boolean }>((subscriber) => {
      this.statusConnection
        .subscribe((connection) => {
          connection?.on(ChatMehods.GetStatusUpdates, (userId, status) => {
            subscriber.next({ userId, status });
          });
        })
    });
  }

  UpdateUserStatus(status: boolean) {
    return new Observable<boolean>((subscriber) => {
      this.statusConnection
        .subscribe((connection) => {
          connection?.invoke(ChatMehods.UpdateStatus, status)
            .then((isUpdated) => {
              subscriber.next(isUpdated);
            });
        })
    });
  }

  RegisterChatEnterance(chatId: string) {
    return new Observable<boolean>((subscriber) => {
      this.statusConnection
        .subscribe((connection) => {
          connection.invoke(ChatMehods.RegisterChatEnterance, chatId)
            .then((result) => {
              subscriber.next(result);
            })
        })
    });
  }
}
