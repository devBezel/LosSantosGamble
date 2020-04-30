import BotClient from './Client/BotClient';
import { token } from './Config';


const client = new BotClient({ token });
client.start();