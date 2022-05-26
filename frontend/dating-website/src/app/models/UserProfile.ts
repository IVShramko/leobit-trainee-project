import { Regions } from '../enums/regions';
import { Genders } from '../enums/Genders';

export interface UserProfile{
    userName: string
    firstName : string
    lastName : string
    birthDate : string
    gender : Genders
    email : string
    phone : string
    region : Regions
    town : string
    photo : string
}