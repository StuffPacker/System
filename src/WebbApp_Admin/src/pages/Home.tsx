import { Typography } from "antd";
import { Container } from "react-bootstrap";
import GalleryCard from "../Components/GalleryCard";
import "../Styles/homes.css";
const { Title, Paragraph } = Typography;

export function Home() {
  return (
    <>

      <div className="hero">
        <div className="overlay">
          <Container className="hero-text" >
            <Title>StuffPacker</Title>
            <Paragraph> What is StuffPacker?<br />
              <br></br>
              <p>Sometimes it can be difficult to know what to pack in your bag, it can also be difficult to know the weight of the bag.
                With this tool you can organize your packing into lists and categories, get full control of your gadgets and get a good overview of the weight.
                If you pack with this tool, you will soon discover that you can save weight and space on several things.</p>

            </Paragraph>

          </Container>
        </div>
      </div>

      {/* Gallery Section */}
      <div className="gallery">
        <div className="gallery1">
          <h1 className="gallery-heading heading2">Want to Help?</h1>
          <span className="gallery-sub-heading">
            If you want to help with the project you can go to our <a href="https://github.com/StuffPacker/System">Github repository</a> and either code or report suggestions or bugs.
          </span>
          <div className="image-container">
            {/* Gallery Cards */}
            <GalleryCard
              image_src="https://images.unsplash.com/photo-1650295751050-b184e54e177c?crop=entropy&amp;cs=tinysrgb&amp;fit=max&amp;fm=jpg&amp;ixid=M3w5MTMyMXwwfDF8cmFuZG9tfHx8fHx8fHx8MTcwMjMwNDU3NHw&amp;ixlib=rb-4.0.3&amp;q=80&amp;w=600"
              rootClassName="rootClassName"
            />
          </div>
        </div>
      </div>
    </>
  );
};