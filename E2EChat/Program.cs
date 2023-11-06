// dotnet run --project E2EChat/E2EChat.csproj


using E2EChat;

Console.Clear();

Message message = new Message();
Console.WriteLine("Hola. Bienvenido al chat de E2E.");

Console.Write("Por favor, ingrese su nombre de usuario:");
var username = Console.ReadLine();

Console.WriteLine("Bienvenido {0}.", username);

Console.WriteLine("Esta es tu clave pública para que la compartas con tus amigos: {0}", message.PublicKey);
Console.Write("Presione enter para continuar...(Se limpiará la pantalla)");
Console.ReadKey();

Console.Clear();

Console.WriteLine("Iniciando el chat ... ");
Console.WriteLine("Para salir del chat en cualquier momento, escriba 'exit'.");
Console.WriteLine("************************************");

var receiverPublicKey = string.Empty;

while(!string.IsNullOrEmpty(receiverPublicKey))
{
    Console.WriteLine("Ingrese la clave pública del destinatario: ");
    receiverPublicKey = Console.ReadLine();
}

Boolean exit = false;

while (exit == false)
{
    Console.Write("Escriba 1 para leer mensajes, 2 para enviar un mensaje, 3 para salir del chat: ");
    var option = Console.ReadLine();

    if (option == "1")
    {
        Console.WriteLine("Ingrese el mensaje recibido: ");
        var receivedMessage = Console.ReadLine();
        if (!string.IsNullOrEmpty(receivedMessage))
            Console.WriteLine("El mensaje recibido es: \n{0} \n", message.Decrypt(receivedMessage));
    }
    else if (option == "2")
    {
        Console.WriteLine("Ingrese el mensaje a enviar: ");
        var messageToSend = Console.ReadLine();
        if (!string.IsNullOrEmpty(messageToSend))
            Console.WriteLine("El encriptado para enviar es: \n{0} \n", message.Encrypt(messageToSend, receiverPublicKey!));
    }
    else if (option == "3")
        exit = true;
    else
        Console.WriteLine("Opción inválida.");

    Console.WriteLine("Presione enter para continuar...");
    Console.ReadKey();

    if (exit)
        break;
}
