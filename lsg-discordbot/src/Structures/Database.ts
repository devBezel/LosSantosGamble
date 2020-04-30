import { ConnectionManager } from 'typeorm';
import { databaseName } from '../Config';

import { Users } from '../Models/UserDataModel';


const connectionManager: ConnectionManager = new ConnectionManager();
connectionManager.create({
    name: databaseName,
    type: 'sqlite',
    database: '../Data/db.sqlite',
    entities: [
        Users
    ]
});

export default connectionManager;