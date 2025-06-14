export type UserProfileToken = {
  $id?: string;
  UserName: string;
  Email: string;
  Token: string;
};

export type UserProfile = {
  userName: string;
  email: string;
};
