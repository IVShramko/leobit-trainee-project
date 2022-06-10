export interface Criteria
{
    profile: ProfileCriteria
    pageSize: number
    pageIndex: number
}
export interface ProfileCriteria
{
    Gender: boolean
    MinAge: number
    MaxAge: number
    Region: string
    Town: string
}