import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('users')
export class Users {
    @PrimaryGeneratedColumn()
    id!: number;

    @Column({ type: 'varchar', length: 32 })
    userId: string;

    @Column({ type: 'varchar', length: 32 })
    trelloCardId: string;

}