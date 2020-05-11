//------------------------------------------------------------------------
// Name:    relay_ProtocolData.h
// Author:  ProtocolGenerator (by jjuiddong)
// Date:    
//------------------------------------------------------------------------
#pragma once

namespace relay {

using namespace network2;
using namespace marshalling;


	struct Relay_Packet {
		cProtocolDispatcher *pdispatcher;
		netid senderId;
		string str;
	};



}
