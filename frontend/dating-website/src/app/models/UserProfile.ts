import { Regions } from '../enums/regions';

export interface UserProfile{
    id: string | undefined
    userName: string
    firstName: string
    lastName: string
    birthDate: string
    gender: Boolean
    email: string
    phoneNumber: string
    region: Regions
    town: string
    photo: string
}