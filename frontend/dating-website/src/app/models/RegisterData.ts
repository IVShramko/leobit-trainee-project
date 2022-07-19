export interface RegisterDTO
{
    UserName: string
    Email : string
    Password : string
    Profile : RegisterData
}

interface RegisterData
{
    BirthDate : Date
    Gender : boolean
}