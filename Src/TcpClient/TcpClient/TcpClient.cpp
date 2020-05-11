
#include "stdafx.h"
#include "../Protocol/Src/relay_Protocol.h"
#include "../Protocol/Src/relay_ProtocolData.h"
#include "../Protocol/Src/relay_ProtocolHandler.h"


int main()
{
	network2::cNetController netController;
	network2::cTcpClient client;
	relay::c2s_Protocol protocol;
	client.RegisterProtocol(&protocol);

	if (!netController.StartTcpClient(&client, "127.0.0.1", 10002))
	{
		std::cout << "Error!! Connection Server" << std::endl;
		return 0;
	}

	std::cout << "Try Connection~\n";

	int cnt = 0;
	while (!client.IsFailConnection())
	{
		protocol.Relay(network2::SERVER_NETID,
			common::format("text %d", cnt++));
		Sleep(1000);
	}

	std::cout << "Finish~\n";

	return 0;
}
