import { ProfileSearchResult } from "./profileSearchResult"

export interface SearchResultDTO {
    resultsTotal: number
    profiles: ProfileSearchResult[]
}