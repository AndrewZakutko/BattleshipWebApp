export interface Game {
    id: string;
    firstPlayerFieldId?: string;
    secondPlayerFieldId?: string;
    firstPlayerName?: string;
    secondPlayerName?: string;
    nameOfWinner?: string;
    status: string;
    moveCount: number;
    resultInfo?: string;
}

export interface FinishGame {
    gameId: string;
    nameOfWinner: string | undefined;
    resultInfo: string;
}