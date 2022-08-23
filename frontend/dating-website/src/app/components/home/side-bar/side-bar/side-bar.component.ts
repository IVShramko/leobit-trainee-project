import { IChatShortDTO } from './../../../../models/chat/chat-short-dto';
import { ChatService } from './../../../../services/chat-service/chat.service';
import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  constructor(private chatService: ChatService) { }

  chats: IChatShortDTO[];

  ngOnInit(): void {
    this.LoadChats();
  }

  private LoadChats() {
    this.chatService.GetAllChats().pipe(
      tap((chats: IChatShortDTO[]) => this.chats = chats)
    ).subscribe();
  }

}
