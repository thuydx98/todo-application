export interface ApiResult<T>{
    success: boolean;
    result: T;
    errorCode: number;
    errorMessage?: string;
}