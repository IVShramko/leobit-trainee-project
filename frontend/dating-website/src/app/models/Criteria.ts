import { Filters } from './../enums/Filters';
export interface Criteria
{
    profile: ProfileCriteria
    pageSize: number
    pageIndex: number
    Filter: number
}
export interface ProfileCriteria
{
    Gender: boolean
    MinAge: number
    MaxAge: number
    Region: string
    Town: string
}