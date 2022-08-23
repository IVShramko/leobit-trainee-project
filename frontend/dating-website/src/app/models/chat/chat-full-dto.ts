import { IChatMemberShortDTO } from './chat-member-short-dto';

export interface IChatFullDTO {
    id: string
    name: string
    sender: IChatMemberShortDTO
    receivers: IChatMemberShortDTO[]
}