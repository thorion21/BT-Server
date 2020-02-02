using System;
using BT_WorldServer.Factories;
using BT_WorldServer.Packets;
using BT_WorldServer.utils;

namespace BT_WorldServer.WorldServer.Auth
{
    public class Authenticator
    {
        private AuthManager _authManager;

        public Authenticator(AuthManager authManager)
        {
            _authManager = authManager;
        }
        
        public void Login(ref DefaultPacket packet)
        {
            DefaultPacket response;
            LoginPacket loginAttempt = LoginPacket.Deserialize(packet.Buffer);
            
            if (loginAttempt.Username == "gogu" && loginAttempt.Password == "salut")
            {
                /* TODO: Interrogate database */
                
                response = PacketFactory.Build(
                    PacketType.LOGIN_RSP_PKT,
                    new LoginPacketResponse(
                        LoginStatus.LOGIN_SUCCESS,
                        "gogu",
                        "tok"
                        )
                    );
            }
            else
            {
                response = PacketFactory.Build(
                    PacketType.LOGIN_RSP_PKT, new LoginPacketResponse(
                        LoginStatus.LOGIN_FAILED
                    )
                );                
            }

            response.SetPeer(packet.Peer);
            _authManager.AddPacketToSending(response);
        }

        public void Logout(ref DefaultPacket packet)
        {
            DefaultPacket response;
            /*LogoutPacket logoutAttempt = LogoutPacket.Deserialize(packet.Buffer);*/

            response = PacketFactory.Build(
                PacketType.LOGOUT_RSP_PKT, new LogoutPacketResponse(
                    LoginStatus.LOGOUT_ACCEPTED
                    )
            );
            
            response.SetPeer(packet.Peer);
            _authManager.AddPacketToSending(response);
        }
    }
}