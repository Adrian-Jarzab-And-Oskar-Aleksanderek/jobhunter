import { Review } from "./review";

export interface JobOffer {
  id: string;
  title: string;
  description: string;
  reviews: Review[];
}
