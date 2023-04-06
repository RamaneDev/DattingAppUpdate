import { Photo } from "./photo"

export interface Member {
    id: number
    username: string
    gender: string
    age: number
    knowsAs: any
    created: string
    lastActive: string
    introduction: string
    lookingFor: string
    interests: string
    city: string
    country: string
    photoUrl: string
    photos: Photo[]
  }  
  