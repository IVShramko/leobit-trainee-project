export interface RegisterDTO
{
    UserName: string
    Email : string
    Password : string
    Data : RegisterData
}

interface RegisterData
{
    BirthDate : Date
    Gender : boolean
}