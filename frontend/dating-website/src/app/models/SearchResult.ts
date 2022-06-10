export interface SearchResult
{
    resultsTotal: number
    profiles: SearchResultUserProfile[]
}

export interface SearchResultUserProfile
{
    age: number
    firstName: string
    lastName: string
    userName: string
}