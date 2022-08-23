export interface IChatMessageCreateDTO {
    text: string
    chatId: string
    senderId: string
    receiverId: string
    createdAt: Date
}