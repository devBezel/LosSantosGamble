import BotClient from './client/BotClient';
import { token } from './Config';


const client = new BotClient({ token });
client.start();