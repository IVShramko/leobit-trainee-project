import { Observable } from 'rxjs';
import { ACCESS_TOKEN } from './../../constants';
import { TokenManager } from './../../managers/tokenManager';
import { Injectable } from '@angular/core';
import * as Signalr from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  constructor(private tokenManager: TokenManager) { }

  private connections = new Map<string, Signalr.HubConnection>();

  IsHubConnectionOpened(hubName: string) {
    const isOpened = this.connections.has(hubName);
    return isOpened;
  }

  GetHubConnection(hubName: string) {
    return this.connections.get(hubName);
  }

  async StartHubConnection(hubName: string) {
    const connection = new Signalr.HubConnectionBuilder()
      .withUrl('https://localhost:44321/' + hubName, {
        skipNegotiation: true,
        transport: Signalr.HttpTransportType.WebSockets,
        accessTokenFactory: () =>
          this.tokenManager.GetToken(ACCESS_TOKEN) as string
      })
      .build();

    await connection.start();
    this.connections.set(hubName, connection);

    return connection;
  }

  StopHubConnection(hubName: string) {
    const connection = this.connections.get(hubName);
    connection?.stop().then(() => {
      this.connections.delete(hubName);
    });
  }
}
