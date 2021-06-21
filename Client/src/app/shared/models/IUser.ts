export interface IUser {
    email: string;
    userName: string;
    token: string;
}

export interface IUserForAdministration
{
    id: string;
    userName: string;
    userRoles: string[];
    email: string;
    emailConfirmed: boolean;
}

export class UserForUpdate {
    roles: string[];
    id: string;
}