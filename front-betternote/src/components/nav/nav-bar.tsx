import { Container, Flex, Heading } from "@chakra-ui/react";
import { HomeIcon } from "@heroicons/react/24/outline";
const links = [
  { href: "/", label: "Home", icon: HomeIcon },
  { href: "/about", label: "About" },
  { href: "/contact", label: "Contact" },
];

export default function NavBar() {
  return (
    <Container as="nav" maxW={"container.xl"}>
      <Flex align="center">
        <Heading as="h1" size="lg">
          Better Note
        </Heading>
        {links.map((link) => {
          return (
            <a key={link.href} href={link.href}>
              {link.label}
            </a>
          );
        })}
      </Flex>
    </Container>
  );
}
