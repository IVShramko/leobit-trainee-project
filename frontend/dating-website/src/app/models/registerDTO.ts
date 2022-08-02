import { ProfileRegisterDTO } from "./profileRegisterDTO"

export interface RegisterDTO {
    UserName: string
    Email: string
    Password: string
    Profile: ProfileRegisterDTO
}