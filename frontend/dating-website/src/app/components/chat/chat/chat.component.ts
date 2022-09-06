import { StatusService } from './../../../services/status-service/status.service';
import { CommunicationService } from './../../../services/communication-service/communication.service';
import { MessageService } from './../../../services/message-service/message.service';
import { IProfileChatDTO } from './../../../models/profile-chat-dto';
import { IChatMessageDTO } from './../../../models/chat/chat-message-dto';
import { UserService } from 'src/app/services/user-service/user.service';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable, Subscription, tap } from 'rxjs';
import { IChatFullDTO } from './../../../models/chat/chat-full-dto';
import { ChatService } from './../../../services/chat-service/chat.service';
import { SignalRService } from './../../../services/signalr-service/signalr.service';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild, Renderer2 } from '@angular/core';
import { IChatMessageCreateDTO } from 'src/app/models/chat/chat-message-create-dto';
import { Hubs } from 'src/app/enums/hubs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
})
export class ChatComponent implements OnInit, OnDestroy {

  constructor(private signalRService: SignalRService,
    private chatService: ChatService,
    private route: ActivatedRoute,
    private userService: UserService,
    private renderer: Renderer2,
    private messageService: MessageService,
    private communicationService: CommunicationService,
    private statusService: StatusService) {
  }

  @ViewChild('message_box') messageBox: ElementRef;
  chat: IChatFullDTO;
  receivers: IProfileChatDTO[] = [];

  statusUpdate: Observable<{userId: string, status: boolean}>;
  //add all subscriptions and unsubscribe in ondestroy
  subscriptions: Subscription[];

  ngOnInit() {
    this.route.params.subscribe(
      (params: Params) => this.LoadChat(params.id)
    );

    this.statusUpdate = this.statusService.GetUserStatus();
    

    //to messaging service

    this.communicationService.ReceiveMessage().subscribe(
      (message) => this.DisplayMessage(message)
    );

    //unsubscribe manually?

  }

  DisplayStatus(status: boolean | undefined){
    return status ? 'online' : 'offline'
  }

  private LoadChat(id: string) {
    this.chatService.GetChatById(id)
      .pipe(
        tap((chat: IChatFullDTO) => this.chat = chat),
        tap(() => this.LoadChatMembersProfile()),
        tap(() => this.LoadChatMessages()),
        tap(this.statusService.RegisterChatEnterance(id))
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
          this.DisplayMessage(message);
        })
      })
    ).subscribe(
      () => this.ScrollToLastMessage()
    );
  }

  private ScrollToLastMessage() {
    const elemToFocus = (<HTMLElement>this.messageBox.nativeElement)
      .lastElementChild;

    elemToFocus?.scrollIntoView();
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

  Send(text: string) {
    for (const receiver of this.chat.receivers) {
      const newMessage = this.CreateMessageForSending(text, receiver.aspNetUserId);

      //unsubscribe manually?
      this.communicationService.SendMessage(newMessage)
        .pipe(
          tap((isSent) => {
            if (isSent) {
              //where put sending to db
              this.DisplayMessage(newMessage);
              this.ScrollToLastMessage();
            }
          })).subscribe();
    }
  }

  ngOnDestroy(): void {
    this.signalRService.StopHubConnection(Hubs.Chat);
  }

}
