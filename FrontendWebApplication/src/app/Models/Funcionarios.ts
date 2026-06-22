export interface Funcionario {
    id?: number;
    nome: string;
    sobrenome: string;
    departamento: string;
    turno: string;
    status: boolean;
    dataCriacao?: string;
    dataAlteracao?: string;
    dataAtualizacao?: string;
}