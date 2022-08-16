import { ACCESS_TOKEN } from './../../constants';
import { TokenManager } from './../../managers/tokenManager';
import { Injectable } from '@angular/core';
import * as Signalr from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  constructor(private tokenManager: TokenManager) { }

  connection: Signalr.HubConnection;

  async StartConnection() {
    this.connection = new Signalr.HubConnectionBuilder()
      .withUrl('https://localhost:44321/chat', {
        skipNegotiation: true,
        transport: Signalr.HttpTransportType.WebSockets,
        accessTokenFactory: () =>
          this.tokenManager.GetToken(ACCESS_TOKEN) as string
      })
      .build();

    await this.connection.start();
  }

  async StopConnection() {
    await this.connection.stop();
  }
}
