//------------------------------------------------------------------------
// Name:    relay_Protocol.h
// Author:  ProtocolGenerator (by jjuiddong)
// Date:    
//------------------------------------------------------------------------
#pragma once

namespace relay {

using namespace network2;
using namespace marshalling;
static const int c2s_Protocol_ID = 100;

class c2s_Protocol : public network2::iProtocol
{
public:
	c2s_Protocol() : iProtocol(c2s_Protocol_ID) {}
	void Relay(netid targetId, const string &str);
};
}
