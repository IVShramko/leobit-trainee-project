import { SignalRService } from './../../../services/signalr-service/signalr.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {

  constructor(private SignalRService: SignalRService) { }

  async ngOnInit() {
    this.SignalRService.StartConnection();

    this.SignalRService.connection.on("ReceiveMessage", (message) => {
      console.log(message)
    })
  }

  async Send() {
    const message =
      (document.querySelector('#message') as HTMLInputElement).value;

    const receiver =
      (document.querySelector('#receiver') as HTMLInputElement).value;
    await this.SignalRService.connection.invoke("SendToAllAsync", message, receiver);
  }

  ngOnDestroy(): void {
    this.SignalRService.StopConnection();
  }

}
