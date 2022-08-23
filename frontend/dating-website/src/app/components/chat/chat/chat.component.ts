import { MessageService } from './../../../services/message-service/message.service';
import { IProfileChatDTO } from './../../../models/profile-chat-dto';
import { IChatMessageDTO } from './../../../models/chat/chat-message-dto';
import { UserService } from 'src/app/services/user-service/user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { tap } from 'rxjs';
import { IChatFullDTO } from './../../../models/chat/chat-full-dto';
import { ChatService } from './../../../services/chat-service/chat.service';
import { SignalRService } from './../../../services/signalr-service/signalr.service';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, Renderer2, ViewEncapsulation } from '@angular/core';
import { IChatMessageCreateDTO } from 'src/app/models/chat/chat-message-create-dto';
import { ChatMehods } from 'src/app/enums/chat-methods';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {

  constructor(private SignalRService: SignalRService,
    private chatService: ChatService,
    private route: ActivatedRoute,
    private userService: UserService,
    private renderer: Renderer2,
    private messageService: MessageService) {
  }

  @ViewChild('message_box') messageBox: ElementRef;
  chat: IChatFullDTO;
  receivers: IProfileChatDTO[] = [];

  async ngOnInit() {
    this.route.params.subscribe(
      (params: Params) => this.LoadChat(params.id)
    );

    this.SignalRService.StartConnection();

    this.SignalRService.connection.on("ReceiveMessage", (message: IChatMessageDTO) => {
      this.DisplayMessage(message);
    })
  }

  private LoadChat(id: string) {
    this.chatService.GetChatById(id)
      .pipe(
        tap((chat: IChatFullDTO) => this.chat = chat),
        tap(() => this.LoadChatMembersProfile()),
        tap(() => this.LoadChatMessages())
      ).subscribe();
  }

  private LoadChatMembersProfile() {
    for (const receiver of this.chat.receivers) {
      this.userService.GetChatProfile(receiver.aspNetUserId)
        .pipe(
          tap((profile) => this.receivers.push(profile))
        ).subscribe();
    }
  }

  private LoadChatMessages() {
    this.messageService.GetAllChatMessages(this.chat.id).pipe(
      tap((messages: IChatMessageDTO[]) => {
        messages.forEach((message) => {
          this.DisplayMessage(message)
        })
      })
    ).subscribe();
  }

  private CreateMessageElement(sender: string, text: string, isReceived: boolean) {
    const elem: HTMLElement = this.messageBox.nativeElement;
    const newMessage = document.createElement('div');
    newMessage.innerHTML = [sender, text].join(' : ')
    this.renderer.addClass(newMessage, 'chat-message');
    const messageAlign = isReceived ? 'start' : 'end';
    this.renderer.setStyle(newMessage, 'align-self', messageAlign);
    this.renderer.appendChild(elem, newMessage);
  }

  private DisplayMessage(message: IChatMessageDTO) {
    let sender: string = 'you';
    let isRecieved = false;

    if (message.senderId !== this.chat.sender.aspNetUserId) {
      sender = this.receivers.find((receiver) => {
        return receiver.aspNetUserId === message.senderId
      })?.userName as string;

      isRecieved = true;
    }

    this.CreateMessageElement(sender, message.text, isRecieved)
  }

  //make one method for creating messages for db and for sending
  CreateMessageForDb(text: string, receiverId: string) {
    const newMessage: IChatMessageCreateDTO = {
      text: text,
      chatId: this.chat.id,
      senderId: this.chat.sender.id,
      createdAt: new Date(),
      receiverId: receiverId
    }

    return newMessage;
  }

  CreateMessageForSending(text: string, receiverId: string) {
    const newMessage: IChatMessageDTO = {
      text: text,
      chatId: this.chat.id,
      senderId: this.chat.sender.aspNetUserId,
      createdAt: new Date(),
      receiverId: receiverId
    }

    return newMessage;
  }

  async Send(text: string) {
    for (const receiver of this.chat.receivers) {

      const newMessage = this.CreateMessageForSending(text, receiver.aspNetUserId);

      this.SignalRService.connection
        .invoke(ChatMehods.SendToUser, newMessage);

      this.SignalRService.connection.on(ChatMehods.GetMessageDeliveryStatus, (status: number) => {
        if (status === 0) {
          this.DisplayMessage(newMessage);
          const dbMessage = this.CreateMessageForDb(text, receiver.id);
          this.messageService.CreateMessage(dbMessage).subscribe();
        }
        this.SignalRService.connection.off(ChatMehods.GetMessageDeliveryStatus)
      })
    }
  }

  ngOnDestroy(): void {
    this.SignalRService.StopConnection();
  }

}
