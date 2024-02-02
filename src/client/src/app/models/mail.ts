export interface Mail {
  id: number;
  for: string;
  subject: string;
  content: string;
  createdAt: Date;
}
